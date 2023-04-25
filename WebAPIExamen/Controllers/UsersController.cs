using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIExamen.Attributes;
using WebAPIExamen.Models;
using WebAPIExamen.ModelsDTOs;

namespace WebAPIExamen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly AnswersDBContext _context;

        public UsersController(AnswersDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }
        [HttpGet("GetUserData")]
        public ActionResult<IEnumerable<UserDTO>> GetUserData(string username) 
        {
            var query = ( from u in _context.Users
                           where u.UserName == username  
                           select new
                           {
                                  UsuarioID  = u.UserId,
                                 UsuarioNombre = u.UserName,
                                   PrimerNombre = u.FirstName,
                                     Apellidos=u.LastName,
                                       Numero = u.PhoneNumber,
                                     Contrasennia =u.UserPassword,
                                    contadordemalas=u.StrikeCount,
                                    correorespaldo=u.BackUpEmail,
                                  trabajoDescripcion= u.JobDescription   
                           }).ToList();
            List<UserDTO>list= new List<UserDTO>();
            foreach (var item in query)
            {
                UserDTO NewItem = new UserDTO()
                {
                    UsuarioID = item.UsuarioID,
                    UsuarioNombre = item.UsuarioNombre,
                    PrimerNombre = item.PrimerNombre,
                    Apellidos = item.Apellidos,
                    Numero = item.Numero,
                    Contrasennia = item.Contrasennia,
                    contadordemalas = item.contadordemalas,
                    correorespaldo = item.correorespaldo,
                    trabajoDescripcion = item.trabajoDescripcion

                    
                };
                list.Add(NewItem);
            }

            if (list==null)
            {
                return NotFound();

            }
            return list;
        
        
        
        }

       [HttpGet("ValidateUserLogin")]
       public async Task<ActionResult<User>> ValidateUserLogin(string pUserName,string pPassword)
        {
            var user = await _context.Users.SingleOrDefaultAsync(e => e.UserName == pUserName && e.UserPassword == pPassword);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }




        // GET: api/Users/5
        [HttpGet("{GetUser}")]
        
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{PutUser}")]
         
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'AnswersDBContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
