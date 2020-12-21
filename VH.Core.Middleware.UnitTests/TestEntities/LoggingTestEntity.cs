using System;

namespace VH.Core.Middleware.UnitTests.TestEntities
{
    public class LoggingTestEntity
    {
        public LoggingTestEntity(Guid hearingRefId, string caseType, DateTime scheduledDateTime, string caseNumber,
            string caseName, string hearingVenueName, bool audioRecordingRequired, string ingestUrl)
        {
            Id = Guid.NewGuid();
            MeetingRoom = new LoggingTestChildEntity();

            HearingRefId = hearingRefId;
            CaseType = caseType;
            ScheduledDateTime = scheduledDateTime;
            CaseNumber = caseNumber;
            CaseName = caseName;
            ClosedDateTime = null;
            HearingVenueName = hearingVenueName;
            AudioRecordingRequired = audioRecordingRequired;
            IngestUrl = ingestUrl;
        }

        public Guid Id { get; protected set; }
        public Guid HearingRefId { get; private set; }
        public string CaseType { get; private set; }
        public DateTime ScheduledDateTime { get; private set; }
        public DateTime? ActualStartTime { get; private set; }
        public DateTime? ClosedDateTime { get; private set; }
        public string CaseNumber { get; private set; }
        public string CaseName { get; private set; }
        public LoggingTestChildEntity MeetingRoom { get; private set; }
        public string HearingVenueName { get; private set; }
        public bool AudioRecordingRequired { get; set; }
        public string IngestUrl { get; set; }
    }
}
