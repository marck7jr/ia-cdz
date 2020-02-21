using CDZ.Common.Helpers;
using CDZ.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDZ.Common.IA
{
    public class BattleIA
    {
        private List<Knight> _goldKnights;
        private List<Knight> _bronzeKnigths;
        private List<Gene> _darwin;

        public BattleIA(List<Knight> goldKnights, List<Knight> bronzeKnights)
        {
            _goldKnights = goldKnights;
            _bronzeKnigths = bronzeKnights;
            _darwin = new List<Gene>();
        }

        private Gene InitializeGene()
        {
            return new Gene
            {
                Battles = new List<Battle>
                {
                    new Battle
                    {
                        GoldKnight = _goldKnights[0],
                        BronzeKnights = new List<Knight>(_bronzeKnigths)
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[1],
                        BronzeKnights = new List<Knight>(_bronzeKnigths)
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[2],
                        BronzeKnights = new List<Knight>(_bronzeKnigths)
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[3],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[0]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[4],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[0]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[5],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[1]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[6],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[1]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[7],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[2]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[8],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[2]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[9],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[3]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[10],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[3]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = _goldKnights[11],
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[4]
                        }
                    },
                    new Battle
                    {
                        GoldKnight = new Knight { Constellation = "Grande Mestre", Name = "Grande Mestre" },
                        BronzeKnights = new List<Knight>
                        {
                            _bronzeKnigths[4]
                        }
                    },
                }
            };
        }
        private void MoveKnightRandomly(List<Battle> battles)
        {
            Battle originBattle;
            Battle destinyBattle;
            Knight choosenKnight;

            while ((originBattle = battles[new Random().Next(battles.Count)]).BronzeKnights.Count == 1) ; ;
            choosenKnight = originBattle.BronzeKnights[new Random().Next(originBattle.BronzeKnights.Count)];
            originBattle.BronzeKnights.Remove(choosenKnight);

            while ((destinyBattle = battles[new Random().Next(battles.Count)]).BronzeKnights.Any(x => x.Name == choosenKnight.Name) || destinyBattle.BronzeKnights.Count == _bronzeKnigths.Count) ; ;
            destinyBattle.BronzeKnights.Add(choosenKnight);
        }

        private void InitializeDarwin()
        {
            for (int i = 0; i < _bronzeKnigths.Count; i++)
            {
                var genX = InitializeGene();
                MoveKnightRandomly(genX.Battles);
                _darwin.Add(genX);
            }
            _darwin.OrderBy(x => x.EstimatedTotalTime);
        }

        public Task<Gene> GetResultAsync(double mapEstimatedTimeResult)
        {
            InitializeDarwin();

            int count = 0;
            int limit = 12 * 60;

            bool firstAndLastAreDifferents;
            bool firstEstimatedTotalTimeWithMapTimeResultIsHigherThenTwelweHours;
            bool countIsBelowLimit;

            void EvaulateConditions()
            {
                firstAndLastAreDifferents = _darwin.First().EstimatedTotalTime != _darwin.Last().EstimatedTotalTime;
                firstEstimatedTotalTimeWithMapTimeResultIsHigherThenTwelweHours = (_darwin.First().EstimatedTotalTime + mapEstimatedTimeResult) > limit;
                countIsBelowLimit = count < (limit / 12);
            }

            EvaulateConditions();
            while (firstAndLastAreDifferents || firstEstimatedTotalTimeWithMapTimeResultIsHigherThenTwelweHours && countIsBelowLimit)
            {
                for (int i = 0; i < _bronzeKnigths.Count; i++)
                {
                    for (int j = 0; j < 300; j++)
                    {
                        var mutation = _darwin[i].Clone();
                        MoveKnightRandomly(mutation.Battles);
                        _darwin.Add(mutation);
                    }
                }

                _darwin = _darwin.OrderBy(x => x.EstimatedTotalTime)
                    .Take(_bronzeKnigths.Count)
                    .ToList();

                count++;
                EvaulateConditions();
            }

            return Task.FromResult(_darwin.FirstOrDefault());
        }
    }
}
