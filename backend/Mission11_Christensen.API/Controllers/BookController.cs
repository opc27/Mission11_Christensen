using Microsoft.AspNetCore.Mvc;
using Mission11_Christensen.API.Data;

namespace Mission11_Christensen.API.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private BookDbContext _bookContext;
    
    public BookController(BookDbContext temp) => _bookContext = temp;

    [HttpGet("AllBooks")]
    public IActionResult GetBooks(int pageSize = 10, int pageNum = 1, [FromQuery] string sortOrder = "asc")
    {
        var something = _bookContext.Books.AsQueryable();
        
        // for sorting
        if (sortOrder.ToLower() == "asc")
        {
            something = something.OrderBy(b => b.Title);
        }
        else if (sortOrder.ToLower() == "desc")
        {
            something = something.OrderByDescending(b => b.Title);
        }
    
        // pull all books
        var books = something
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var totalNumBooks = _bookContext.Books.Count();

        var someObject = new
        {
            Books = books,
            TotalNumBooks = totalNumBooks
        };
        
        return Ok(someObject);
    }
}