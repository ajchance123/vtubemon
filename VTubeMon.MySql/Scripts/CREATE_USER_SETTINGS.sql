CREATE TABLE `vtube_mon_db`.`user_settings` (
  `id_user_settings` INT NOT NULL AUTO_INCREMENT,
  `setting_name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id_user_settings`, `setting_name`),
  UNIQUE INDEX `id_user_settings_UNIQUE` (`id_user_settings` ASC) VISIBLE,
  UNIQUE INDEX `setting_name_UNIQUE` (`setting_name` ASC) VISIBLE);
