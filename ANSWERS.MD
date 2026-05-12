
# Part 3 Submission(question 1)

1. CREATE TABLE Applications (
    Id GUID PRIMARY KEY,
    JobId GUID NOT NULL,
    CandidateId GUID NOT NULL,
    Stage ENUM NOT NULL,
    CoverLetter TEXT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW(),

    CONSTRAINT FK_Applications_Job FOREIGN KEY (JobId) REFERENCES Jobs(Id),
    CONSTRAINT FK_Applications_Candidate FOREIGN KEY (CandidateId) REFERENCES Candidates(Id)
);

## INDEXES
CREATE INDEX IX_Applications_JobId ON Applications(JobId);
CREATE INDEX IX_Applications_CandidateId ON Applications(CandidateId);
CREATE UNIQUE INDEX UX_Applications_Job_Candidate ON Applications(JobId, CandidateId);

## WHY
fast job → applications lookup
fast candidate → applications lookup
prevent duplicate applications 


2. CREATE TABLE ApplicationNotes (
    Id GUID PRIMARY KEY,
    ApplicationId GUID NOT NULL,
    Type ENUM NOT NULL,
    Description TEXT NOT NULL,
    CreatedBy GUID NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW(),

    FOREIGN KEY (ApplicationId) REFERENCES Applications(Id),
    FOREIGN KEY (CreatedBy) REFERENCES TeamMembers(Id)
);

## INDEXES
CREATE INDEX IX_Notes_ApplicationId ON ApplicationNotes(ApplicationId);
CREATE INDEX IX_Notes_CreatedBy ON ApplicationNotes(CreatedBy);

## WHY
fast load notes per application

3. CREATE TABLE StageHistories (
    Id GUID PRIMARY KEY,
    ApplicationId GUID NOT NULL,
    FromStage ENUM NOT NULL,
    ToStage ENUM NOT NULL,
    Updated_By GUID NOT NULL,
    Reason TEXT NULL,
    Updated_At TIMESTAMP DEFAULT NULL,

    FOREIGN KEY (ApplicationId) REFERENCES Applications(Id),
    FOREIGN KEY (ChangedBy) REFERENCES TeamMembers(Id)
);

## INDEXES
CREATE INDEX IX_StageHistory_ApplicationId ON StageHistories(ApplicationId);
CREATE INDEX IX_StageHistory_ChangedAt ON StageHistories(ChangedAt);

## WHY
Audit transitions 

4. CREATE TABLE ApplicationScores (
    Id GUID PRIMARY KEY,
    ApplicationId GUID NOT NULL,
    Type ENUM NOT NULL,   -- CultureFit, Interview, Assessment
    Score INT NOT NULL,
    Comment TEXT NULL,
    TeamMemberId GUID NOT NULL,
    CreatedAt TIMESTAMP NOT NULL DEFAULT NOW(),
    Updated_At TIMESTAMP DEFAULT NULL,
    UpdatedBy Guid DEFAULT NULL,

    FOREIGN KEY (ApplicationId) REFERENCES Applications(Id),
    FOREIGN KEY (TeamMemberId) REFERENCES TeamMembers(Id)
);

## INDEXES
CREATE INDEX IX_Scores_ApplicationId ON ApplicationScores(ApplicationId);
CREATE INDEX IX_Scores_Type ON ApplicationScores(Type);
CREATE INDEX IX_Scores_TeamMemberId ON ApplicationScores(TeamMemberId);

## WHY
Audit who scored what and which category, thus whether interview, culture-fit or assessment



The query runs against the Applications table as the root entity and retrieves related data from Candidates, ApplicationScores, ApplicationNotes, and StageHistories, each linked by ApplicationId or navigation properties.

EF Core translates the entire LINQ expression into a single SQL query with correlated subqueries rather than multiple separate queries.

Number of round trips: 1 database round trip, since all data is retrieved in a single generated SQL query.