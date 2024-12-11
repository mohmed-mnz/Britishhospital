if  not exists (select * from sys.columns where Name = N'OrgId' and Object_ID = Object_ID(N'Counters'))
 begin
	alter table Counter
	add OrgId int  null;
	alter table Counter
	add constraint FK_Counter_Org foreign key (OrgId) references Org(Id) ;
	end