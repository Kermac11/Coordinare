CREATE TABLE [dbo].[Bookings] (
    [Booking_ID]    INT      NOT NULL,
    [Event_ID]      INT      NOT NULL,
    [User_ID]       INT      NOT NULL,
    [Special_Seat]  BIT  NOT NULL,
    [InWaitingList] BIT  NOT NULL,
    [BookingDate]   DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Booking_ID] ASC),
    CONSTRAINT [FK_Bookings_Event] FOREIGN KEY ([Event_ID]) REFERENCES [dbo].[Events] ([Event_ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_User] FOREIGN KEY ([User_ID]) REFERENCES [dbo].[Users] ([User_ID]) ON DELETE CASCADE
);

