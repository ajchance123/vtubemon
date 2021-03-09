﻿CREATE SCHEMA vtube_mon_db;

CREATE TABLE vtube_mon_db.agencies (
	id_agency INT NOT NULL AUTO_INCREMENT,
	name VARCHAR(45) NULL,
	PRIMARY KEY (id_agency));

INSERT INTO vtube_mon_db.agencies
	(name)
	VALUES
	('hololive');

CREATE TABLE vtube_mon_db.vtubers (
	id_vtubers INT NOT NULL AUTO_INCREMENT,
	en_name VARCHAR(45) NULL,
	jp_name VARCHAR(45) NULL,
	channel_link VARCHAR(120) NULL,
	debut_datetime_utc DATETIME NULL,
	affiliation INT NULL,
	is_independent BIT(1) NULL,
	generation INT NULL,
	PRIMARY KEY (id_vtubers),
	UNIQUE INDEX id_vtubers_UNIQUE (id_vtubers ASC) VISIBLE);

USE vtube_mon_db;
	ALTER TABLE vtubers ADD CONSTRAINT fk_vtubers_affiliation_name FOREIGN KEY (affiliation) REFERENCES agencies(id_agency);

INSERT INTO vtube_mon_db.vtubers
	(en_name, jp_name, channel_link, debut_datetime_utc, affiliation, is_independent, generation)

VALUES

	('Tokino Sora',			'ときのそら',	'https://www.youtube.com/channel/UCp6993wxpyDPHUpavwDFqgg', '2017-09-07', 1, 0, 0),
	('Roboco',				'ロボ子',		'https://www.youtube.com/channel/UCDqI2jOz0weumE8s7paEk6g', '2018-03-04', 1, 0, 0),
	('Sakura Miko',			'さくらみこ',	'https://www.youtube.com/channel/UC-hM6YJuNYVAmUWxeIr9FeA', '2018-08-01', 1, 0, 0),
	('Hoshimachi Suisei',	'星街すいせい',	'https://www.youtube.com/channel/UC5CwaMl1eIgY8h02uZw7u8A', '2018-03-22', 1, 0, 0),
	('Virtual Diva AZKi',	'AZKi',			'https://www.youtube.com/channel/UC0TXe_LYZ4scaW2XMyi5_kw', '2018-11-15', 1, 0, 0),

	('Yozora Mel',			'夜空メル',		'https://www.youtube.com/channel/UCD8HOxPs4Xvsm8H0ZxXGiBw', '2018-05-13', 1, 0, 1),
	('Shirakami Fubuki',	'白上フブキ',	'https://www.youtube.com/channel/UCdn5BQ06XqgXoAxIhbqw5Rg', '2018-06-01', 1, 0, 1),
	('Natsuiro Matsuri',	'夏色まつり',	'https://www.youtube.com/channel/UCQ0UDLQCjY0rmuxCDE38FGg', '2018-06-01', 1, 0, 1),
	('Aki Rosenthal','アキ・ローゼンタール',	'https://www.youtube.com/channel/UCFTLzh12_nrtzqBPsTCqenA', '2018-06-07', 1, 0, 1),
	('Akai Haato',			'赤井はあと',	'https://www.youtube.com/channel/UC1CfXB_kRs3C-zaeTG3oGyg', '2018-06-02', 1, 0, 1),
