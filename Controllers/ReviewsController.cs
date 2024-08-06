using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace PriceProphet.Controllers
{
    public class ReviewsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FetchReviews(string productLink)
        {
            var productId = ExtractProductId(productLink);
            if (string.IsNullOrEmpty(productId))
            {
                ViewBag.Error = "Invalid Amazon product link.";
                return View("Index");
            }

            var reviews = await GetReviewsAsync(productId);
            ViewBag.Reviews = reviews;
            ViewBag.ProductId = productId;

            return View("ReviewResults");
        }


        private string ExtractProductId(string url)
        {
            var regex = new Regex(@"[\/dp\/|\/gp\/product\/|\/gp\/aw\/d\/|\/gp\/slredirect\/p\/|\/o\/ASIN\/|\/dp\/|\/gp\/offer-listing\/|\/gp\/movers-and-shakers\/|\/gp\/bestsellers\/|\/gp\/new-releases\/|\/gp\/most-wished-for\/|\/gp\/gift-ideas\/|\/gp\/|\/dp\/|\/o\/dp\/|\/o\/offer-listing\/](?<id>[A-Z0-9]{10})");
            var match = regex.Match(url);
            return match.Success ? match.Groups["id"].Value : string.Empty;
        }


        private Task<List<string>> GetReviewsAsync(string productId)
        {
            // Simulate fetching reviews
            var reviews = new List<string>
        {
            "This product is amazing! I love it.",
            "I had a terrible experience with this product. Would not recommend.",
            "It's okay, does the job but nothing special.",
            "Great value for the price!",
            "Not worth the money. Poor quality."
        };

            return Task.FromResult(reviews);
        }

    }
}
