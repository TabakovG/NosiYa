﻿using NosiYa.Web.ViewModels;

namespace NosiYa.Services.Data
{
	using Microsoft.EntityFrameworkCore;

	using NosiYa.Data;
	using NosiYa.Data.Models;
	using Interfaces;
	using Models;
	using Web.ViewModels.Event;
    using System.Net;

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
		        Name = WebUtility.HtmlEncode(model.Name),
		        Description = WebUtility.HtmlEncode(model.Description),
		        Location = WebUtility.HtmlEncode(model.Location),
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
                .Where(e => e.IsActive)
                .Where(e=>e.IsApproved)
                .OrderBy(e=>e.EventStartDate)
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
					OwnerId = e.OwnerId.ToString(),
                    EventStartDate = e.EventStartDate.Date,
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


        public async Task<AllEventsPagedServiceModel> AllUnavailableEventsByUserIdAsync(AllEventsPaginatedModel model, string userId)
        {
	        var events = context
		        .Events
		        .AsNoTracking()
		        .Where(e => e.IsActive && e.IsApproved == false)
		        .Where(e => e.OwnerId.ToString() == userId)
		        .OrderBy(e => e.EventStartDate)
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
			        OwnerId = e.OwnerId.ToString(),
			        EventStartDate = e.EventStartDate.Date,
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

        public async Task<bool> IsApprovedByIdAsync(int id)
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
		        .Include(i => i.Owner)
		        .Where(e => e.IsActive) 
		        .FirstAsync(e => e.Id == id);

	        var model = new EventDetailsViewModel
	        {
		        Id = evnt.Id,
		        Name = evnt.Name,
		        Description = evnt.Description,
		        Location = evnt.Location,
		        OwnerId = evnt.OwnerId.ToString(),
		        Owner = evnt.Owner.UserName, 
		        EventStartDate = evnt.EventStartDate,
		        EventEndDate = evnt.EventEndDate,
		        Images = evnt.Images.Select(i => i.Url).ToArray()
	        };
            return model;
        }

        public async Task<IEnumerable<EventApprovalViewModel>> GetAllForApproval()
        {
			return await this.context
				.Events
				.AsNoTracking()
				.Where(e => e.IsApproved == false && e.IsActive)
				.Select(e => new EventApprovalViewModel()
				{
					EventName = e.Name,
					EventStart = e.EventStartDate.ToString("HH:mm dd-MM-yyyy"),
					EventEnd = e.EventEndDate.ToString("HH:mm dd-MM-yyyy"),
					EventId = e.Id,
					UserName = e.Owner.UserName,
				})
				.ToArrayAsync();
		}

        //Update:

        public async Task<EventFormModel> GetForEditByIdAsync(int id)
        {
	        return await this.context
		        .Events
		        .Where(e => e.IsActive) 
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
		        .Where(e => e.IsActive) 
		        .Where(e => e.Id == id)
		        .FirstAsync();

	        evnt.Name = WebUtility.HtmlEncode(model.Name);
            evnt.Description = WebUtility.HtmlEncode(model.Description);
            evnt.Location = WebUtility.HtmlEncode(model.Location);
            evnt.EventStartDate = model.EventStartDate;
            evnt.EventEndDate = model.EventEndDate;
            evnt.IsApproved = false;

            await this.context.SaveChangesAsync();
        }

        public async Task ApproveByIdAsync(int id)
        {
	        var evnt = await this.context
		        .Events
		        .Where(e => e.Id == id)
		        .FirstAsync();

			evnt.IsApproved = true;
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
