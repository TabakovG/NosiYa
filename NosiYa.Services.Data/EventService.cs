namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using Interfaces;
	using Model;
	using Web.ViewModels.Event;

    public class EventService : IEventService
    {
        private readonly NosiYaDbContext context;

        public EventService(NosiYaDbContext _context)
        {
            context = _context;
        }

        //Create:

        public Task<int> CreateAndReturnIdAsync(EventFormModel model)
        {
            throw new NotImplementedException();
        }

        //Read: 

        public async Task<AllEventsPagedServiceModel> AllAvailableEventsAsync(AllEventsPaginatedModel model)
        {
            var events = context
                .Events
                .AsNoTracking()
                .Where(e => e.IsActive)
                .AsQueryable();

            int evetCount = events.Count();

            var eventModels = await events
                .Where(e => e.EventStartDate.Date >= DateTime.UtcNow.Date)
                .OrderBy(e => e.EventStartDate)
                .Skip((model.CurrentPage - 1) * model.EventsPerPage)
                .Take(model.EventsPerPage)
                .Select(e => new EventAllViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    EventStartDate = e.EventStartDate.Date, //TODO to specific format ?
                    EventEndDate = e.EventEndDate.Date,
                    ImageUrl = e.Images
                        .Where(i => i.IsDefault)
                        .Select(i => i.Url)
                        .FirstOrDefault() ?? string.Empty
                })
                .ToArrayAsync();


            return new AllEventsPagedServiceModel
            {
                EventsCount = evetCount,
                Events = eventModels
            };
        }

        public Task<bool> ExistsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<EventDetailsViewModel> GetDetailsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        //Update:

        public Task<EventFormModel> GetForEditByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditByIdAsync(int id, EventFormModel model)
        {
            throw new NotImplementedException();
        }

        //Delete:

        public Task DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
