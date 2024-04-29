//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Project.Models;
//using Project.Data;

//namespace Project.Controller.Controllers
//{
//    public class CardController : ControllerBase
//    {

//        private readonly BankContext _context;

//        public CardController(BankContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Card
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Card>>> GetCard()
//        {
//            return await _context.Cards.ToListAsync();
//        }

//        // GET: api/Card/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Card>> GetCard(Guid id)
//        {
//            var Card = await _context.Cards.FindAsync(id);

//            if (Card == null)
//            {
//                return NotFound();
//            }

//            return Card;
//        }

//        // POST: api/Card
//        [HttpPost]
//        public async Task<ActionResult<Card>> CreateCard(Card card)
//        {
//            _context.Cards.Add(card);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetCard", new { id = card.Id }, card);
//        }

//        // PUT: api/Card/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateCard(Guid id, Card card)
//        {
//            if (id != card.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(card).State = EntityState.Modified;
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        // DELETE: api/Card/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCard(Guid id)
//        {
//            var card = await _context.Cards.FindAsync(id);
//            if (card == null)
//            {
//                return NotFound();
//            }

//            _context.Cards.Remove(card);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}

