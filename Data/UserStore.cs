using WebApplication1.Models.Dto;

namespace WebApplication1.Data
{
    public static class UserStore
    {
        public static List<UserDTO> userList = new List<UserDTO>
        {
             new UserDTO{Id=1, Username="kakung", Password="bangkit12"},
             new UserDTO{Id=2, Username="kath", Password="12345678"}
        };

    }
}
