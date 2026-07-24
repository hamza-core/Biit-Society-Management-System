using System;
using System.Collections.Generic;
using project.Models;

namespace project.Services
{
    public class EventParticipant
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string AridNo { get; set; }
        public string StudentName { get; set; }
        public int ParticipantNumber { get; set; }
        public bool HasCard { get; set; }
        public bool AttendanceMarked { get; set; }
        public DateTime? AttendanceTime { get; set; }
    }

    public class EventService
    {
        private static Dictionary<int, List<EventParticipant>> _eventParticipants = new Dictionary<int, List<EventParticipant>>();
        private static int _participantIdCounter = 1;

        public List<EventParticipant> GetParticipantsByEvent(int eventId)
        {
            if (!_eventParticipants.ContainsKey(eventId))
            {
                return new List<EventParticipant>();
            }
            return _eventParticipants[eventId];
        }

        public EventParticipant RegisterParticipant(int eventId, string aridNo, string studentName)
        {
            if (!_eventParticipants.ContainsKey(eventId))
            {
                _eventParticipants[eventId] = new List<EventParticipant>();
            }

            // Check if already registered
            var existing = _eventParticipants[eventId].Find(p => p.AridNo == aridNo);
            if (existing != null)
            {
                return existing;
            }

            var participant = new EventParticipant
            {
                Id = _participantIdCounter++,
                EventId = eventId,
                AridNo = aridNo,
                StudentName = studentName,
                ParticipantNumber = _eventParticipants[eventId].Count + 1,
                HasCard = false,
                AttendanceMarked = false
            };

            _eventParticipants[eventId].Add(participant);
            return participant;
        }

        public void MarkAttendanceByQR(string qrData)
        {
            // QR Data format: aridNo|eventName|participantNumber
            var parts = qrData.Split('|');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Invalid QR code format");
            }

            var aridNo = parts[0];
            var eventName = parts[1];
            var participantNumber = int.Parse(parts[2]);

            // Find event by name (in real app, use event ID)
            foreach (var kvp in _eventParticipants)
            {
                var participant = kvp.Value.Find(p => 
                    p.AridNo == aridNo && p.ParticipantNumber == participantNumber);
                
                if (participant != null && !participant.AttendanceMarked)
                {
                    participant.AttendanceMarked = true;
                    participant.AttendanceTime = DateTime.Now;
                    participant.HasCard = true;
                    break;
                }
            }
        }

        public bool GenerateCardForParticipant(int eventId, string aridNo)
        {
            var participants = GetParticipantsByEvent(eventId);
            var participant = participants.Find(p => p.AridNo == aridNo);
            
            if (participant != null)
            {
                participant.HasCard = true;
                return true;
            }
            return false;
        }

        public int GetTotalParticipants(int eventId)
        {
            if (!_eventParticipants.ContainsKey(eventId))
            {
                return 0;
            }
            return _eventParticipants[eventId].Count;
        }

        public int GetPresentCount(int eventId)
        {
            if (!_eventParticipants.ContainsKey(eventId))
            {
                return 0;
            }
            return _eventParticipants[eventId].FindAll(p => p.AttendanceMarked).Count;
        }

        public List<EventParticipant> GetParticipantsWithoutCards(int eventId)
        {
            if (!_eventParticipants.ContainsKey(eventId))
            {
                return new List<EventParticipant>();
            }
            return _eventParticipants[eventId].FindAll(p => !p.HasCard);
        }
    }
}
