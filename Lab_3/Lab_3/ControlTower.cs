using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    public class ControlTower : IControlTower
    {
        private Dictionary<Aircraft, (Facility?, string)> _aircrafts = new Dictionary<Aircraft, (Facility?, string)>();
        private List<Terminal> _terminals = new List<Terminal>();
        private List<Runway> _runways = new List<Runway>();
        private PriorityQueue<Aircraft, int> _requestQueue = new PriorityQueue<Aircraft, int>();
        public IReadOnlyDictionary<Aircraft, (Facility?, string)> Aircrafts => _aircrafts;
        public IReadOnlyList<Runway> Runways => _runways;
        public IReadOnlyList<Terminal> Terminals => _terminals;
        public async Task ProcessRequest(Aircraft aircraft)
        {
            if (_aircrafts[aircraft].Item1 != null)
            {
                _requestQueue.Enqueue(aircraft, 1);
                Console.Write($"{aircraft.RegCode} requested taking off");
            }
            else
            {
                _requestQueue.Enqueue(aircraft, 2);
                Console.Write($"{aircraft.RegCode} requested landing off");
            }
            _aircrafts[aircraft] = (_aircrafts[aircraft].Item1, "queued");
            await ProcessRequest();
        }
        private async Task ProcessRequest()
        {
            while (_requestQueue.Count > 0)
            {
                var aircraft = _requestQueue.Peek();
                bool isLanded = _aircrafts[aircraft].Item1 != null;

                var freeRunway = _runways.FirstOrDefault(r => !r.IsOccupied);
                var freeTerminal = _terminals.FirstOrDefault(t => !t.IsOccupied);

                if (!(freeRunway != null && (isLanded || freeTerminal != null)))
                {
                    await Task.Delay(1000);
                    continue;
                }

                aircraft = _requestQueue.Dequeue();

                freeRunway.Reserve();

                if (isLanded)
                {
                    _aircrafts[aircraft].Item1.Free();
                    _aircrafts[aircraft] = (freeRunway, "taking off");
                    Console.Write($"{aircraft.RegCode} is taking off from Runway-{_runways.IndexOf(freeRunway)}");
                }
                else
                {
                    freeTerminal.Reserve();
                    _aircrafts[aircraft] = (freeRunway, "landing");
                    Console.Write($"{aircraft.RegCode} is landing on Runway-{_runways.IndexOf(freeRunway)}");
                }

                await Task.Delay(10000);

                if (isLanded)
                {
                    _aircrafts[aircraft] = (null, "");
                    Console.Write($"{aircraft.RegCode} in air");
                }
                else
                {
                    _aircrafts[aircraft] = (freeTerminal, "");
                    Console.Write($"{aircraft.RegCode} at Terminal-{_terminals.IndexOf(freeTerminal)}");
                }
                freeRunway.Free();
            }
        }
        public void AddTerminal()
        {
            _terminals.Add(new Terminal());
        }
        public void AddRunway()
        {
            _runways.Add(new Runway());
        }
        public Aircraft AddAircraft(string regCode)
        {
            if (_aircrafts.Keys.Any(a => a.RegCode == regCode))
                throw new ArgumentException($"Airplane with registration code '{regCode}' already exists.");
            Aircraft aircraft = new Aircraft(regCode, this);
            _aircrafts[aircraft] = (null, "");
            Console.Write($"{aircraft.RegCode} registered");
            return aircraft;
        }
        public void RemoveAircraft(Aircraft aircraft)
        {
            Console.Write($"{aircraft.RegCode} left");
            if (_aircrafts.ContainsKey(aircraft))
                _aircrafts.Remove(aircraft);
        }
    }
}
