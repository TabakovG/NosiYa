using NosiYa.Web.ViewModels;

namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using NosiYa.Data.Models;
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
		        .Where(e => e.IsActive )
		        .AnyAsync(e => e.Id == id);
        }

        public async Task<bool> ApprovedByIdAsync(int id)
        {
			return await this.context
				.Events
				.Where(e => e.IsApproved)
				.AnyAsync(e => e.Id == id);
		}

        public async Task<bool> IsOwnedByUserAsync(int id, string userId)
        {
	        return await this.context
		        .Events
		        .Where(e => e.Id == id)
		        .Select(e => e.OwnerId.ToString() == userId)
		        .FirstAsync();
        }

        public async Task<EventDetailsViewModel> GetDetailsByIdAsync(int id)
        {
	        var evnt = await this.context
		        .Events
		        .AsNoTracking()
		        .Include(i => i.Images)
		        .Where(e => e.IsActive && e.IsApproved) 
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

        public async Task<EventDetailsViewModel> GetDetailsForAdminByIdAsync(int id)
        {
			var evnt = await this.context
				.Events
				.AsNoTracking()
				.Include(i => i.Images)
				.Where(e => e.IsActive) 
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

        public async Task<IEnumerable<ApprovalViewModel>> GetAllForApproval()
        {
			return await this.context
				.Events
				.AsNoTracking()
				.Where(e => e.IsApproved == false && e.IsActive)
				.Select(e => new ApprovalViewModel
				{
					DetailsPath = "/Event/Details/",
					Element = e.Name,
					ElementId = e.Id.ToString(),
					UserName = e.Owner.UserName,
				})
				.ToArrayAsync();
		}

        //Update:

        public async Task<EventFormModel> GetForEditByIdAsync(int id)
        {
	        return await this.context
		        .Events
		        .Where(e => e.IsActive && e.IsApproved) //TODO to separate active from approved in another method
		        .Where(e => e.Id == id)
		        .Select(e => new EventFormModel
		        {
			        Name = e.Name,
			        Description = e.Description,
			        Location = e.Location,
			        EventStartDate = e.EventStartDate,
			        EventEndDate = e.EventEndDate,
		        })
		        .FirstAsync();
        }

        public async Task EditByIdAsync(int id, EventFormModel model)
        {
	        var evnt = await this.context
		        .Events
		        .Where(e => e.IsActive && e.IsApproved) //TODO to separate active from approved in another method
		        .Where(e => e.Id == id)
		        .FirstAsync();

	        evnt.Name = model.Name;
            evnt.Description = model.Description;
            evnt.Location = model.Location;
            evnt.EventStartDate = model.EventStartDate;
            evnt.EventEndDate = model.EventEndDate;

            await this.context.SaveChangesAsync();
        }

        public async Task<EventForDeleteViewModel> GetForDeleteByIdAsync(int id)
        {
	        return await this.context
		        .Events
		        .AsNoTracking()
		        .Include(i=>i.Images)
		        .Where(e => e.IsActive && e.Id == id)
		        .Select(e => new EventForDeleteViewModel
		        {
			        Name = e.Name,
			        Description = e.Description,
			        ImageUrl = e.Images
				        .Where(i => i.IsDefault)
				        .Select(i => i.Url)
				        .FirstOrDefault() ?? string.Empty
		        })
		        .FirstAsync();
        }

        //Delete:

        public async Task DeleteByIdAsync(int id)
        {
            var eventToDelete = await this.context
	            .Events
	            .Where(e => e.IsActive)
	            .FirstAsync(e => e.Id == id);

			eventToDelete.IsActive = false;

			await this.context.SaveChangesAsync();
        }
    }
}
