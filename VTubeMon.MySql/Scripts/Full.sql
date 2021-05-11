CREATE SCHEMA IF NOT EXISTS vtube_mon_db;

DROP TABLE IF EXISTS vtube_mon_db.user_settings_values;
DROP TABLE if EXISTS vtube_mon_db.user_settings_details;
DROP TABLE IF EXISTS vtube_mon_db.user_settings_main;
DROP TABLE IF EXISTS vtube_mon_db.vtubers_images;
DROP TABLE IF EXISTS vtube_mon_db.store_item;
DROP TABLE IF EXISTS vtube_mon_db.inventory_item;
DROP TABLE IF EXISTS vtube_mon_db.item_stat;
DROP TABLE IF EXISTS vtube_mon_db.item;
DROP TABLE IF EXISTS vtube_mon_db.item_category;
DROP TABLE IF EXISTS vtube_mon_db.stat_category;
DROP TABLE IF EXISTS vtube_mon_db.dailies; 
DROP TABLE IF EXISTS vtube_mon_db.vtubers;
DROP TABLE IF EXISTS vtube_mon_db.users;
DROP TABLE IF EXISTS vtube_mon_db.agencies;


CREATE TABLE vtube_mon_db.agencies (
	id_agency INT NOT NULL AUTO_INCREMENT,
	agency_name VARCHAR(45) NULL,
	PRIMARY KEY (id_agency));

CREATE TABLE `vtube_mon_db`.`users` (
  `id_user` BIGINT UNSIGNED NOT NULL,
  `id_guild` BIGINT UNSIGNED NOT NULL,
  `admin` BOOLEAN NOT NULL,
  `vtuber_cash` INT NULL,
  PRIMARY KEY (`id_user`, `id_guild`));

CREATE TABLE vtube_mon_db.dailies (
  id_user BIGINT UNSIGNED NOT NULL,
  id_guild BIGINT UNSIGNED NOT NULL,
  check_in_date DATETIME NOT NULL,
  PRIMARY KEY (id_user, id_guild, check_in_date),
  CONSTRAINT fk_dailies_id_users
    FOREIGN KEY (id_user, id_guild)
    REFERENCES vtube_mon_db.users (id_user, id_guild));

CREATE TABLE vtube_mon_db.vtubers (
	id_vtubers INT NOT NULL AUTO_INCREMENT,
	en_name VARCHAR(45) NULL,
	jp_name VARCHAR(45) NULL,
	channel_link VARCHAR(120) NULL,
	debut_datetime_utc DATETIME NULL,
	id_agency INT NULL,
	is_independent BIT(1) NULL,
	generation INT NULL,
	PRIMARY KEY (id_vtubers),
	UNIQUE INDEX id_vtubers_UNIQUE (id_vtubers ASC) VISIBLE,
	CONSTRAINT fk_vtubers_id_agency 
		FOREIGN KEY (id_agency) 
        REFERENCES agencies(id_agency));

CREATE TABLE vtube_mon_db.vtubers_images (
  id_vtuber_image INT NOT NULL AUTO_INCREMENT,
  id_vtuber INT NOT NULL,
  image_path VARCHAR(256) NOT NULL,
  PRIMARY KEY (id_vtuber_image),
  INDEX vtuber_image_id_vituber_idx (id_vtuber ASC) VISIBLE,
  UNIQUE INDEX image_path_UNIQUE (image_path ASC) VISIBLE,
  UNIQUE INDEX id_vtuber_image_UNIQUE (id_vtuber_image ASC) VISIBLE,
  CONSTRAINT fk_vtubers_images_vtubers_id_vituber
    FOREIGN KEY (id_vtuber)
    REFERENCES vtube_mon_db.vtubers (id_vtubers)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE vtube_mon_db.user_settings_main (
  id_user_settings_main INT NOT NULL AUTO_INCREMENT,
  setting_name VARCHAR(45) NOT NULL,
  PRIMARY KEY (id_user_settings_main),
  UNIQUE INDEX setting_name_UNIQUE (setting_name ASC) VISIBLE);

CREATE TABLE vtube_mon_db.user_settings_details (
  id_user_settings_details INT NOT NULL AUTO_INCREMENT,
  id_user_settings_main INT NOT NULL,
  detail VARCHAR(256) NOT NULL,
  PRIMARY KEY (id_user_settings_details),
  UNIQUE INDEX id_user_settings_details_option_UNIQUE (detail ASC) VISIBLE,
  CONSTRAINT fk_id_user_settings_details_on_id_user_settings_main
    FOREIGN KEY (id_user_settings_main)
    REFERENCES vtube_mon_db.user_settings_main (id_user_settings_main)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE vtube_mon_db.user_settings_values (
  id_user_settings_values INT NOT NULL AUTO_INCREMENT,
  id_user_settings_main INT NOT NULL,
  id_user BIGINT UNSIGNED NOT NULL,
  id_guild BIGINT UNSIGNED NOT NULL,
  value VARCHAR(256) NULL,
  PRIMARY KEY (id_user_settings_values),
  INDEX id_user_settings_main_fk_idx (id_user_settings_main ASC) VISIBLE,
  INDEX id_user_fk_idx (id_user ASC) VISIBLE,
  CONSTRAINT fk_user_settings_values_on_id_user_settings_main
    FOREIGN KEY (id_user_settings_main)
    REFERENCES vtube_mon_db.user_settings_main (id_user_settings_main)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_user_settings_values_on_id_user_and_id_guild
    FOREIGN KEY (id_user, id_guild)
    REFERENCES vtube_mon_db.users (id_user, id_guild)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
    
CREATE TABLE vtube_mon_db.item_category(
	id_category int NOT NULL PRIMARY KEY,
    category_name varchar(64)
);

CREATE TABLE vtube_mon_db.item (
	id_item int NOT NULL PRIMARY KEY,
    item_name varchar(64) UNIQUE,
    id_category int NOT NULL,
    price int NOT NULL,
    image_path varchar(256) DEFAULT NULL,
    FOREIGN KEY (id_category) REFERENCES item_category(id_category)
);

CREATE TABLE vtube_mon_db.stat_category(
	id_stat int NOT NULL PRIMARY KEY,
    stat_name varchar(64) UNIQUE
);

CREATE TABLE vtube_mon_db.item_stat (
	id_item int NOT NULL,
    id_stat int NOT NULL,
    stat_value int NOT NULL,
    PRIMARY KEY (id_item, id_stat),
    FOREIGN KEY (id_item) REFERENCES item(id_item),
    FOREIGN KEY (id_stat) REFERENCES stat_category(id_stat)
);

CREATE TABLE vtube_mon_db.inventory_item (
	id_item int NOT NULL,
    id_user BIGINT UNSIGNED NOT NULL,
    id_guild BIGINT UNSIGNED NOT NULL,
    item_quantity int NOT NULL,
    PRIMARY KEY (id_item, id_user, id_guild),
    FOREIGN KEY (id_item) REFERENCES item(id_item),
    FOREIGN KEY (id_user) REFERENCES users(id_user)
);
    
CREATE TABLE vtube_mon_db.store_item (
  id_item int NOT NULL,
  item_buy_limit int NOT NULL,
  item_quantity int NOT NULL,
  PRIMARY KEY (id_item),
  FOREIGN KEY (id_item) REFERENCES item(id_item)
);

INSERT INTO vtube_mon_db.agencies
	(agency_name)
VALUES
	('hololive'),
	('nijisanji');

INSERT INTO vtube_mon_db.vtubers
	(en_name, jp_name, channel_link, debut_datetime_utc, id_agency, is_independent, generation)
VALUES
	('Tokino Sora',			'ときのそら',			'https://www.youtube.com/channel/UCp6993wxpyDPHUpavwDFqgg',		'2017-09-07', 1, 0, 0),
	('Roboco',				'ロボ子',				'https://www.youtube.com/channel/UCDqI2jOz0weumE8s7paEk6g',		'2018-03-04', 1, 0, 0),
	('Sakura Miko',			'さくらみこ',			'https://www.youtube.com/channel/UC-hM6YJuNYVAmUWxeIr9FeA',		'2018-08-01', 1, 0, 0),
	('Hoshimachi Suisei',	'星街すいせい',			'https://www.youtube.com/channel/UC5CwaMl1eIgY8h02uZw7u8A',		'2018-03-22', 1, 0, 0),
	('Virtual Diva AZKi',	'AZKi',					'https://www.youtube.com/channel/UC0TXe_LYZ4scaW2XMyi5_kw',		'2018-11-15', 1, 0, 0),

	('Yozora Mel',			'夜空メル',				'https://www.youtube.com/channel/UCD8HOxPs4Xvsm8H0ZxXGiBw',		'2018-05-13', 1, 0, 1),
	('Shirakami Fubuki',	'白上フブキ',			'https://www.youtube.com/channel/UCdn5BQ06XqgXoAxIhbqw5Rg',		'2018-06-01', 1, 0, 1),
	('Natsuiro Matsuri',	'夏色まつり',			'https://www.youtube.com/channel/UCQ0UDLQCjY0rmuxCDE38FGg',		'2018-06-01', 1, 0, 1),
	('Aki Rosenthal',		'アキ・ローゼンタール',	'https://www.youtube.com/channel/UCFTLzh12_nrtzqBPsTCqenA',		'2018-06-07', 1, 0, 1),
	('Akai Haato',			'赤井はあと',			'https://www.youtube.com/channel/UC1CfXB_kRs3C-zaeTG3oGyg',		'2018-06-02', 1, 0, 1),
	('Minato Aqua',			'湊あくあ(みなとあくあ)', 'https://www.youtube.com/channel/UC1opHUrw8rvnsadT-iGp7Cg',		'2018-08-08', 1, 0, 2),
    ('Murasaki Shion',		'紫咲シオン',			'https://www.youtube.com/channel/UCXTpFs_3PqI41qX2d9tL2Rw',		'2018-08-17', 1, 0, 2),
    ('Nakiri Ayame',		'百鬼あやめ',			'https://www.youtube.com/channel/UC7fk0CB07ly8oSl0aqKkqFg',		'2018-09-03', 1, 0, 2),
    ('Yuzuki Choco',		'癒月ちょこ',			'https://www.youtube.com/channel/UC1suqwovbL1kzsoaZgFZLKg',		'2018-09-05', 1, 0, 2),
    ('Oozora Subaru',		'大空スバル',			'https://www.youtube.com/channel/UCvzGlP9oQwU--Y0r9id_jnA',		'2018-09-16', 1, 0, 2);
	;
	
INSERT INTO 
	vtube_mon_db.vtubers_images
	(id_vtuber, image_path)
VALUES
	(1, 'Tokino_Sora_2020_Portrait.png');
	
    
INSERT INTO vtube_mon_db.user_settings_main
	(setting_name)
VALUES
	('Language');

INSERT INTO vtube_mon_db.user_settings_details
	(id_user_settings_main, detail)
VALUES
	(1, 'English'),
	(1, '日本語');

INSERT INTO item_category(id_category, category_name) VALUES (0, 'Consumable');
INSERT INTO item_category(id_category, category_name) VALUES (1, 'Headgear');
INSERT INTO stat_category(id_stat, stat_name) VALUES (0, 'Strength');
INSERT INTO stat_category(id_stat, stat_name) VALUES (1, 'Speed');
INSERT INTO item(id_item, item_name, id_category, price) VALUES (0, 'Daimond Tiara', 1, 50);
INSERT INTO item_stat(id_item, id_stat, stat_value) VALUES (0, 0, 10);
INSERT INTO item_stat(id_item, id_stat, stat_value) VALUES (0, 1, 5);
INSERT INTO store_item(id_item, item_buy_limit, item_quantity) VALUES (0, 5, 100);