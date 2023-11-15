using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Dto;

namespace WebApplication1.Controllers
{
    [Route("api/UserAPI")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetVillas()
        {
            return Ok(UserStore.userList);
        }

        [HttpGet("{Id:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(404)]

        public ActionResult<UserDTO> GetUser(int id)
        {
            if (id == 0) return BadRequest();
            var acc = UserStore.userList.FirstOrDefault(u => u.Id == id);
            if (acc == null) return NotFound();
            return Ok(acc);
        }

        [HttpPost]
        public ActionResult<UserDTO> CreateAcc([FromBody] UserDTO userDTO)
        {
            if (UserStore.userList.FirstOrDefault(u => u.Username.ToLower() == userDTO.Username.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Account already exists");
                return BadRequest(ModelState);
            }

            if (userDTO == null)
            {
                return BadRequest(userDTO);
            }

            if (userDTO.Id != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            userDTO.Id = UserStore.userList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            UserStore.userList.Add(userDTO);
            string response = "Sukses menambahkan data Akun" + "\nNama : " + userDTO.Username;
            return CreatedAtRoute("GetUser", new { id = userDTO.Id }, response);
        }

        [HttpDelete("{id:int}", Name = "DeleteUser")]
        public IActionResult DeleteUser(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var acc = UserStore.userList.FirstOrDefault(u => u.Id == id);
            if (acc == null)
            {
                return NotFound();
            }
            UserStore.userList.Remove(acc);
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        public IActionResult UpdateUser(int id, [FromBody] UserDTO userDTO)
        {
            if (userDTO == null || id != userDTO.Id)
            {
                return BadRequest();
            }
            var acc = UserStore.userList.FirstOrDefault(u => u.Id == id);
            acc.Username = userDTO.Username;
            acc.Password = userDTO.Password;
            return NoContent();
        }
    }
};
