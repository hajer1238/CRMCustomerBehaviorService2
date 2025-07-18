using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRMCustomerBehavior.Models; // TRÈS IMPORTANT : Permet d'utiliser la classe CustomerEvent

namespace CRMCustomerBehavior.Controllers
{
    [ApiController] // Indique que c'est un contrôleur API
    [Route("[controller]")] // Définit la route de base pour ce contrôleur (ici, /CustomerEvents)
    public class CustomerEventsController : ControllerBase
    {
        private readonly ILogger<CustomerEventsController> _logger; // Pour enregistrer les messages (logs)

        public CustomerEventsController(ILogger<CustomerEventsController> logger)
        {
            _logger = logger;
        }

        [HttpPost] // Indique que cette méthode répond aux requêtes HTTP de type POST
        public IActionResult LogEvent([FromBody] CustomerEvent ev) // Reçoit un CustomerEvent du corps de la requête
        {
            // Ceci va enregistrer une information détaillée dans ta console (ton premier log !)
            _logger.LogInformation("Customer {CustomerId} performed {EventType} at {Timestamp}",
                                    ev.CustomerId, ev.EventType, ev.Timestamp);

            return Ok(); // Retourne une réponse "200 OK" (succès)
        }
    }
}