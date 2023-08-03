namespace NosiYa.Data.Configurations
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static NosiYa.Common.SeedingConstants;

    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(this.CreateEvent());
        }

        private Event[] CreateEvent()
        {
            return new Event[]
            {
                new Event
                {
                    Id = 1,
                    Name = "Фестивал на фолклорната носия - Жеравна",
                    Description = "Фестивалът е създаден през 2008 година по идея на Христо Димитров – продуцент, хореограф и режисьор на Национален фолклорен ансамбъл „Българе”. Атрактивната им сватба с фолклорната певица Албена Вескова през 2005 г. в местността \"Костина\" край с. Рибарица по старинен български обичай, на която младоженците и присъстващите 400 гости са с български народни носии, има широк позитивен отзвук. Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.",
                    Location = "42.8336195015454, 26.45904413725397",
                    IsApproved = true,
                    OwnerId = Guid.Parse(AdminId),
                    EventStartDate = DateTime.Parse("07-07-2023"),
                    EventEndDate = DateTime.Parse("07-07-2023"),
                },
                new Event
                {
                    Id = 2,
                    Name = "Фестивал на  Жеравна",
                    Description = "Това мотивира създателя на „Българе” с помощта на неговите партньори Ян и Елена Андерсон, на тогавашния кмет на Жеравна Лъчезар Германов, бизнесмените Георги Манев и Калин Григоров, както и на други съмишленици, да организира много по-мащабно събитие, което да дава възможност на всеки желаещ поне за три дни в годината да облече българска носия и се откъсне от цивилизацията, като направи скок век и половина назад във времето.",
                    Location = "42.8336195015454, 26.45904413725397",
                    IsApproved = false,
                    OwnerId = Guid.Parse(UserId),
                    EventStartDate = DateTime.Parse("07-07-2023"),
                    EventEndDate = DateTime.Parse("07-07-2023"),
                }
            };
        }
    }
}
