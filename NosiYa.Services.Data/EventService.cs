using NosiYa.Data.Models;

namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using Interfaces;
	using Models;
	using Web.ViewModels.Event;

    public class EventService : IEventService
    {
        private readonly NosiYaDbContext context;

        public EventService(NosiYaDbContext _context)
        {
            context = _context;
        }

        //Create:

        public async Task<int> CreateAndReturnIdAsync(EventFormModel model, Guid userId)
        {
	        var newEvent = new Event
	        {
		        Name = model.Name,
		        Description = model.Description,
		        Location = model.Location,
		        OwnerId = userId,
		        EventStartDate = model.EventStartDate,
		        EventEndDate = model.EventEndDate,
	        };

	        await this.context.Events.AddAsync(newEvent);
            await this.context.SaveChangesAsync();

            return newEvent.Id;
        }

        //Read: 

        public async Task<AllEventsPagedServiceModel> AllAvailableEventsAsync(AllEventsPaginatedModel model)
        {
            var events = context
                .Events
                .AsNoTracking()
                .Where(e => e.IsActive && e.IsApproved)
                .AsQueryable();

            int eventsCount = events.Count();

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
                EventsCount = eventsCount,
                Events = eventModels
            };
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
	        return await this.context
		        .Events
		        .Where(e => e.IsActive && e.IsApproved) //TODO to separate active from approved in another method
		        .AnyAsync(e => e.Id == id);
        }

        public async Task<EventDetailsViewModel> GetDetailsByIdAsync(int id)
        {
	        var evnt = await this.context
		        .Events
		        .AsNoTracking()
		        .Include(i => i.Images)
		        .Where(e => e.IsActive && e.IsApproved) //TODO to separate active from approved in another method
		        .FirstAsync(e => e.Id == id);

	        var model = new EventDetailsViewModel
	        {
		        Id = evnt.Id,
		        Name = evnt.Name,
		        Description = evnt.Description,
		        Location = evnt.Location,
		        OwnerId = evnt.OwnerId,
		        Owner = evnt.Owner, //TODO do I need the owner or only the id as string ?
		        EventStartDate = evnt.EventStartDate,
		        EventEndDate = evnt.EventEndDate,
		        Images = evnt.Images.Select(i => i.Url).ToArray()

			};
            return model;
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
