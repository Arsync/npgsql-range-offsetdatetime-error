using NodaTime;
using NpgsqlTypes;

namespace InstantIssueApp.Models
{
    public class SampleModel
    {
        public int Id { get; set; }

        public NpgsqlRange<OffsetDateTime> VersionPeriod { get; set; }

        public OffsetDateTime CreationTime { get; set; }
    }
}
