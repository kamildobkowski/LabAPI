using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.DomainEvents.Order;
using LabAPI.Domain.Enums;
using LabAPI.Domain.Repositories;
using LabAPI.Domain.ValueObjects;
using LabAPI.Infrastructure.Services.Email;
using MediatR;

namespace LabAPI.Application.Features.Orders.Events;

public sealed class OrderResultsConfirmedHandler(ITestRepository testRepository, IPdfService pdfService, 
    IOrderRepository orderRepository, IEmailService emailService, ICustomerRepository customerRepository) 
    : INotificationHandler<OrderResultsConfirmed>
{
    public async Task Handle(OrderResultsConfirmed notification, CancellationToken cancellationToken)
    {
        var model = new OrderResultDocumentModel
        {
            OrderNumber = notification.Order.OrderNumber,
            Date = notification.Order.CreatedAt,
            PatientData = notification.Order.PatientData,
            TestResults = []
        };
        foreach (var i in notification.Order.Results)
        {
            var test = await testRepository.GetAsync(r => r.ShortName == i.Key);
            var testResult = new OrderResultDocumentModel.TestResult
            {
                Name = test!.Name,
                ShortName = test.ShortName,
                Markers = []
            };
            foreach (var j in i.Value!)
            {
                var markerResult =
                    new OrderResultDocumentModel.MarkerResult(
                        test.Markers.FirstOrDefault(r => r.ShortName == j.Key)!, j.Value);
                testResult.Markers.Add(markerResult);
            }
            model.TestResults.Add(testResult);
        }
        await pdfService.CreateOrderPdf(notification.Order, model);
        notification.Order.Status = OrderStatus.PdfReady;
        await orderRepository.SaveChangesAsync();
        var customer = await customerRepository.GetAsync(r => r.Pesel == notification.Order.PatientData.Pesel);
        if(customer is not null) 
            _ = emailService.SendResultReadyEmail(customer.Email, customer.Name, customer.Surname);
        
    }
}