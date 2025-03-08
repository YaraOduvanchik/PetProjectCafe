namespace PetProjectCafe.Application.OrderFeatures.Queries.GetAllByPeriodDateTime;

public record GetAllByFilteredCommand(
    DateTime TimeIntervalFrom, 
    DateTime TimeIntervalTo,
    string Status);