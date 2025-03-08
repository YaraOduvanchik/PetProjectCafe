namespace PetProjectCafe.API.Controllers.Requests.Orders;

public record GetAllByFilteredRequest(
    DateTime TimeIntervalFrom, 
    DateTime TimeIntervalTo,
    string Status);