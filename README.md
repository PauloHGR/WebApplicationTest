# WebApplicationTest

This is an example of how Rabbit MQ is used and configurated in .NET application. There are two entities (Order and Product),
where product is associated with order, whose data are save in a Mongo Db instance. RabbitMq is used to put the request data in a queue, and consuming
this data. A API is implemented as well, in order to get information about Product and Order.
