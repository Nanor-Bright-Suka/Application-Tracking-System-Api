
using ATS.Enums;

namespace ATS.Helpers;

public static class ApplicationStageValidator
{
    private static readonly Dictionary<ApplicationStageEnum, List<ApplicationStageEnum>> ValidTransitions
        = new()
        {
            {
                ApplicationStageEnum.Applied,
                new List<ApplicationStageEnum>
                {
                    ApplicationStageEnum.Screening,
                    ApplicationStageEnum.Rejected
                }
            },

            {
                ApplicationStageEnum.Screening,
                new List<ApplicationStageEnum>
                {
                    ApplicationStageEnum.Interview,
                    ApplicationStageEnum.Rejected
                }
            },

            {
                ApplicationStageEnum.Interview,
                new List<ApplicationStageEnum>
                {
                    ApplicationStageEnum.Offer,
                    ApplicationStageEnum.Rejected
                }
            },

            {
                ApplicationStageEnum.Offer,
                new List<ApplicationStageEnum>
                {
                    ApplicationStageEnum.Hired,
                    ApplicationStageEnum.Rejected
                }
            },

            {
                ApplicationStageEnum.Hired,
                new List<ApplicationStageEnum>()
            },

            {
                ApplicationStageEnum.Rejected,
                new List<ApplicationStageEnum>()
            }
        };

    public static bool IsValidTransition(
        ApplicationStageEnum currentStage,
        ApplicationStageEnum targetStage)
    {
        return ValidTransitions[currentStage].Contains(targetStage);
    }
}