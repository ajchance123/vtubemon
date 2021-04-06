CREATE TABLE `vtube_mon_db`.`vtubers_images` (
  `id_vtuber_image` INT NOT NULL,
  `id_vtuber` INT NOT NULL,
  `image_path` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`id_vtuber_image`),
  INDEX `vtuber_image_id_vituber_idx` (`id_vtuber` ASC) VISIBLE,
  UNIQUE INDEX `image_path_UNIQUE` (`image_path` ASC) VISIBLE,
  UNIQUE INDEX `id_vtuber_image_UNIQUE` (`id_vtuber_image` ASC) VISIBLE,
  CONSTRAINT `vtuber_image_id_vituber`
    FOREIGN KEY (`id_vtuber`)
    REFERENCES `vtube_mon_db`.`vtubers` (`id_vtubers`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);CREATE TABLE `vtube_mon_db`.`vtubers_images` (
  `id_vtuber_image` INT NOT NULL,
  `id_vtuber` INT NOT NULL,
  `image_path` VARCHAR(256) NOT NULL,
  PRIMARY KEY (`id_vtuber_image`),
  INDEX `vtuber_image_id_vituber_idx` (`id_vtuber` ASC) VISIBLE,
  UNIQUE INDEX `image_path_UNIQUE` (`image_path` ASC) VISIBLE,
  UNIQUE INDEX `id_vtuber_image_UNIQUE` (`id_vtuber_image` ASC) VISIBLE,
  CONSTRAINT `vtuber_image_id_vituber`
    FOREIGN KEY (`id_vtuber`)
    REFERENCES `vtube_mon_db`.`vtubers` (`id_vtubers`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);