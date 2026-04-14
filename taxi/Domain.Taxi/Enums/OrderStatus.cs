namespace Domain.Taxi.Enums;

/// <summary>
/// Represents the status of an order.
/// </summary>
public enum OrderStatus
{
    Searching = 0,      // Ищет водителя
    InProgress = 1,     // В пути
    Completed = 2,      // Завершён
    Cancelled = 3       // Отменён
}