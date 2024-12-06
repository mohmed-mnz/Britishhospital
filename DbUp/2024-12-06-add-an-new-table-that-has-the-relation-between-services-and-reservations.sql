if not  exists (select * from sysobjects where name='ServiceReservation' and xtype='U')
create table ServiceReservation
(
	Id  int identity(1,1) not null,
	ServiceId int not null,
	ReservationId bigint not null,
	constraint FK_ServiceReservation_Service foreign key (ServiceId) references Service(Id) on delete cascade on update cascade,
	constraint FK_ServiceReservation_Reservations foreign key (ReservationId) references Reservations(Id) on delete cascade on update cascade
);