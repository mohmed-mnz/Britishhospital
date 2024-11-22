alter table counters
add  Orgid integer
references organizations(Orgid)
 on delete cascade;	