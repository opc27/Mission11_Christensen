using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission11_Christensen.API.Data;

namespace Mission11_Christensen.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private BookDbContext _bookContext;
    
    public BookController(BookDbContext temp) => _bookContext = temp;

    // [HttpGet("AllBooks")]
    // public IActionResult GetBooks(int pageSize = 10, int pageNum = 1, [FromQuery] string sortOrder = "asc", List<string>? bookTypes = null)
    // {
    //     var query = _bookContext.Books.AsQueryable();
    //
    //     if (bookTypes != null && bookTypes.Any())
    //     {
    //         query = query.Where(b => bookTypes.Contains(b.Category));
    //     }
    //     
    //     // for sorting
    //     if (sortOrder.ToLower() == "asc")
    //     {
    //         query = query.OrderBy(b => b.Title);
    //     }
    //     else if (sortOrder.ToLower() == "desc")
    //     {
    //         query = query.OrderByDescending(b => b.Title);
    //     }
    //
    //     // pull all books
    //     var books = query
    //         .Skip((pageNum - 1) * pageSize)
    //         .Take(pageSize)
    //         .ToList();
    //
    //     var totalNumBooks = _bookContext.Books.Count();
    //
    //     var someObject = new
    //     {
    //         Books = books,
    //         TotalNumBooks = totalNumBooks
    //     };
    //     
    //     return Ok(someObject);
    // }
    
    [HttpGet("AllBooks")]
    public async Task<IActionResult> GetBooks(int pageSize = 10, int pageNum = 1, [FromQuery] string sortOrder = "asc", [FromQuery]List<string>? bookTypes = null)
    {
        // Ensure pageNum and pageSize are within valid ranges
        if (pageNum < 1) pageNum = 1;
        if (pageSize < 1) pageSize = 10;

        // Start with the base query (all books)
        var query = _bookContext.Books.AsQueryable();

        // Apply filtering based on book types (if provided)
        if (bookTypes != null && bookTypes.Any())
        {
            query = query.Where(b => bookTypes.Contains(b.Category));
        }

        // Apply sorting (ascending or descending)
        var sortOrderTrimmed = sortOrder?.Trim().ToLower();
        if (sortOrderTrimmed == "asc")
        {
            query = query.OrderBy(b => b.Title);
        }
        else if (sortOrderTrimmed == "desc")
        {
            query = query.OrderByDescending(b => b.Title);
        }

        // Now, apply pagination after all filters and sorting
        var books = await query
            .Skip((pageNum - 1) * pageSize)  // Calculate the number of records to skip
            .Take(pageSize)                  // Limit to the page size
            .ToListAsync();                  // Execute query and retrieve data

        // Get the total number of books (for pagination metadata)
        var totalNumBooks = await _bookContext.Books.CountAsync();

        // Return the result with pagination metadata
        var result = new
        {
            Books = books,
            TotalNumBooks = totalNumBooks
        };

        return Ok(result);
    }
    
    [HttpGet("GetBookTypes")]
    public IActionResult GetBooksType()
    {
        var bookTypes = _bookContext.Books
            .Select(b => b.Category)
            .Distinct()
            .ToList();
        
        return Ok(bookTypes);
    }
}