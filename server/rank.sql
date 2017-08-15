# 创建数据库
CREATE DATABASE game;

# 切换数据库
USE game;

# 创建数据表
CREATE TABLE `rank` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `_username` varchar(255) NOT NULL,
  `_level` int(11) NOT NULL,
  `_score` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;