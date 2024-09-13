 --organizations
 insert into TransportDb..organization (name, address, is_deleted) values(N'Рога и копыта', N'Дом 1 город Боли', 0);
 insert into TransportDb..organization (name, address, is_deleted) values(N'Химикаты и яды', N'Дом 2 город Любви', 0);
 insert into TransportDb..organization (name, address, is_deleted) values(N'Крик душни', N'Дом 3 Дом', 0);
 insert into TransportDb..organization (name, address, is_deleted) values(N'Фосфат натрия', N'Дом 4 Химиков', 0);
 
 --container types
insert into SdDb..container_type values(N'ПМК 0.75',0.75,0);
insert into SdDb..container_type values(N'ЕК 0.36',0.36,0);
insert into SdDb..container_type values(N'ЕК 0.24',0.24,0);
insert into SdDb..container_type values(N'Бункер 4м3',4.0,0);
insert into SdDb..container_type values(N'Бункер 8м3',8.0,0);
insert into SdDb..container_type values(N'ПЛАСТИК ЕК 0.77',0.77,0);
insert into SdDb..container_type values(N'ПЛАСТИК ЕК 1.1',1.1,0);
insert into SdDb..container_type values(N'ПЛАСТИК ЕК 1.2',1.2,0);
insert into SdDb..container_type values(N'ПЛАСТИК ЕК 0.66',0.66,0);
insert into SdDb..container_type values(N'ЦИНК 0.77',0.77,0);
insert into SdDb..container_type values(N'ЦИНК 1.1',1.1,0);