using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.DataTransferObjects;
using WebApplicationTest.Models;
using WebApplicationTest.RabbitMQ;
using WebApplicationTest.Services;

namespace WebApplicationTest.Controllers
{
    public static class ItemController
    {

        public static void RegisterItemEndpoint(this IEndpointRouteBuilder routes)
        {
            
            routes.MapGet("/products", async (IProductService _productService) =>
            {
                var products = await _productService.GetProductsList();
                return Results.Ok(products);
            })
            .WithName("GetProductsList");

            routes.MapPost("/products", async (IProductService _productService,
                [FromBody]Product request) =>
            {
                var product = await _productService.AddProduct(request);
                return Results.Created("product", product);
            })
            .WithName("AddProduct");

            routes.MapGet("/orders", async (IOrderService _orderService) =>
            {
                var orders = await _orderService.GetOrdersList();
                return Results.Ok(orders);
            });

            routes.MapPost("/orders", async (IOrderService _orderService,
                IRabbitMQProducer _rabbitMQProducer,
                [FromBody] OrderRequest order) =>
            {
                var result = await _orderService.AddOrder(order);
                _rabbitMQProducer.SendProductMessage(result);
                return Results.Created("order",order);
            });

            routes.MapGet("/get-final-value-order/{id}", async (IOrderService _orderService,
                string id) =>
            {
                var orders = await _orderService.GetFinalValueOfOrder(id);
                return Results.Ok(orders);
            });

            routes.MapGet("/get-orders-from-client/{id}", async (IOrderService _orderService,
                int id) =>
            {
                var orders = await _orderService.GetOrderByClientId(id);
                return Results.Ok(orders);
            });

            routes.MapGet("/get-amount-orders-from-client/{id}", async (IOrderService _orderService,
                int id) =>
            {
                var orders = await _orderService.GetOrderByClientId(id);
                return Results.Ok(orders.Count);
            });

        }


    }
}

