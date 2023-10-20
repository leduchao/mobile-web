using MobileWeb.Models.Entities;
using Newtonsoft.Json;

namespace MobileWeb.Services
{
    public class CartService
    {
        public string CARTKEY = "cart";
        private readonly IHttpContextAccessor _context;
        private readonly HttpContext _httpContext;


        public CartService(IHttpContextAccessor context)
        {
            _context = context;
            _httpContext = context.HttpContext!;
        }

        // Lấy cart từ Session (danh sách CartItem)
        public List<CartItems>? GetCartItems()
        {

            var session = _httpContext.Session;
            string? jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItems>>(jsoncart);
            }
            return new List<CartItems>();
        }

        // Xóa cart khỏi session
        public void ClearCart()
        {
            var session = _httpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        public void SaveCartSession(List<CartItems> ls)
        {
            var session = _httpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}
