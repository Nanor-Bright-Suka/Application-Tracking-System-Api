
using ATS.Enums; 
using ATS.Helpers;

namespace ATS.Tests.Applications;

public class ApplicationStageValidatorTests
{
    [Theory]
    [InlineData(ApplicationStageEnum.Applied, ApplicationStageEnum.Screening)]
    [InlineData(ApplicationStageEnum.Applied, ApplicationStageEnum.Rejected)]
    [InlineData(ApplicationStageEnum.Screening, ApplicationStageEnum.Interview)]
    [InlineData(ApplicationStageEnum.Interview, ApplicationStageEnum.Offer)]
    [InlineData(ApplicationStageEnum.Offer, ApplicationStageEnum.Hired)]
    public void IsValidTransition_ShouldReturnTrue_ForValidTransitions(
        ApplicationStageEnum currentStage,
        ApplicationStageEnum targetStage)
    {
     
        var result = ApplicationStageValidator
            .IsValidTransition(currentStage, targetStage);

        Assert.True(result);
    }





    [Theory]
    [InlineData(ApplicationStageEnum.Applied, ApplicationStageEnum.Offer)]
    [InlineData(ApplicationStageEnum.Applied, ApplicationStageEnum.Hired)]
    [InlineData(ApplicationStageEnum.Screening, ApplicationStageEnum.Hired)]
    [InlineData(ApplicationStageEnum.Interview, ApplicationStageEnum.Applied)]
    [InlineData(ApplicationStageEnum.Hired, ApplicationStageEnum.Screening)]
    [InlineData(ApplicationStageEnum.Rejected, ApplicationStageEnum.Applied)]
    public void IsValidTransition_ShouldReturnFalse_ForInvalidTransitions(
    ApplicationStageEnum currentStage,
    ApplicationStageEnum targetStage)
    {
      
        var result = ApplicationStageValidator
            .IsValidTransition(currentStage, targetStage);

     
        Assert.False(result);
    }















}



