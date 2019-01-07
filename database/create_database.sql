/*******************************************************************************
 * Database character set: utf8
 * Server version: 5.5
 * Server version build: 5.5.5-10.3.11-MariaDB
 ******************************************************************************/

/*******************************************************************************
 * Selected metadata objects
 * -------------------------
 * Extracted at 07/01/2019 21:24:38
 ******************************************************************************/

/*******************************************************************************
 * Tables
 * ------
 * Extracted at 07/01/2019 21:24:38
 ******************************************************************************/

CREATE DATABASE ingresso;

CREATE TABLE cidade (
  IdCidade   Integer(11) NOT NULL AUTO_INCREMENT,
  NomeCidade NVarChar(50) COLLATE utf8_general_ci NOT NULL, 
  PRIMARY KEY (
      IdCidade
  )
) ENGINE=InnoDB AUTO_INCREMENT=12 ROW_FORMAT=DYNAMIC DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
ALTER TABLE cidade COMMENT = '';
CREATE TABLE cinema (
  IdCinema   Integer(11) NOT NULL AUTO_INCREMENT,
  NomeCinema NVarChar(100) COLLATE utf8_general_ci,
  Endereco   NVarChar(200) COLLATE utf8_general_ci NOT NULL,
  IdCidade   Integer(11) NOT NULL, 
  PRIMARY KEY (
      IdCinema
  )
) ENGINE=InnoDB AUTO_INCREMENT=27 ROW_FORMAT=DYNAMIC DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
ALTER TABLE cinema COMMENT = '';
CREATE TABLE cinema_sala (
  Id       Integer(11) NOT NULL AUTO_INCREMENT,
  IdCinema Integer(11) NOT NULL,
  IdSala   Integer(11) NOT NULL, 
  PRIMARY KEY (
      Id
  )
) ENGINE=InnoDB AUTO_INCREMENT=3 ROW_FORMAT=DYNAMIC DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
ALTER TABLE cinema_sala COMMENT = '';
CREATE TABLE filme (
  IdFilme            Integer(11) NOT NULL AUTO_INCREMENT,
  NomeFilme          NVarChar(100) COLLATE utf8_general_ci NOT NULL,
  Genero             NVarChar(100) COLLATE utf8_general_ci NOT NULL,
  ClassificacaoIdade Integer(3) NOT NULL,
  Sinopse            NVarChar(1000) COLLATE utf8_general_ci NOT NULL,
  Duracao            Integer(3) NOT NULL, 
  PRIMARY KEY (
      IdFilme
  )
) ENGINE=InnoDB AUTO_INCREMENT=8 ROW_FORMAT=DYNAMIC DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
ALTER TABLE filme COMMENT = '';
CREATE TABLE sala (
  IdSala   Integer(11) NOT NULL AUTO_INCREMENT,
  NomeSala NVarChar(50) COLLATE utf8_general_ci NOT NULL, 
  PRIMARY KEY (
      IdSala
  )
) ENGINE=InnoDB AUTO_INCREMENT=7 ROW_FORMAT=DYNAMIC DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
ALTER TABLE sala COMMENT = '';
CREATE TABLE sessao (
  IdSessao   Integer(11) NOT NULL AUTO_INCREMENT,
  Preco      Float NOT NULL,
  DataSessao Date NOT NULL,
  Hora       DateTime NOT NULL,
  TipoIdioma NVarChar(100) COLLATE utf8_general_ci NOT NULL,
  IdCinema   Integer(11) NOT NULL,
  IdSala     Integer(11) NOT NULL,
  IdFilme    Integer(11) NOT NULL, 
  PRIMARY KEY (
      IdSessao
  )
) ENGINE=InnoDB AUTO_INCREMENT=7 ROW_FORMAT=DYNAMIC DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
ALTER TABLE sessao COMMENT = '';
/*******************************************************************************
 * Indices
 * -------
 * Extracted at 07/01/2019 21:24:38
 ******************************************************************************/

CREATE INDEX i_cidade_ 
 ON cidade(IdCidade);
CREATE INDEX i_cidade_Nome 
 ON cidade(NomeCidade);
CREATE INDEX i_filmes_ 
 ON filme(IdFilme);
/*******************************************************************************
 * Unique Constraints
 * ------------------
 * Extracted at 07/01/2019 21:24:38
 ******************************************************************************/

ALTER TABLE cidade ADD CONSTRAINT unique_Nome UNIQUE 
    (NomeCidade);

/*******************************************************************************
 * Foreign Key Constraints
 * -----------------------
 * Extracted at 07/01/2019 21:24:38
 ******************************************************************************/

ALTER TABLE cinema ADD CONSTRAINT fk_cinema_cidade FOREIGN KEY (IdCidade)
  REFERENCES cidade (IdCidade)
  ON DELETE NO ACTION 
  ON UPDATE NO ACTION;

ALTER TABLE cinema_sala ADD CONSTRAINT fk_cinema_sala_cinema FOREIGN KEY (IdCinema)
  REFERENCES cinema (IdCinema)
  ON DELETE NO ACTION 
  ON UPDATE NO ACTION;

ALTER TABLE cinema_sala ADD CONSTRAINT fk_cinema_sala_sala FOREIGN KEY (IdSala)
  REFERENCES sala (IdSala)
  ON DELETE NO ACTION 
  ON UPDATE NO ACTION;

ALTER TABLE sessao ADD CONSTRAINT fk_sessao_cinema FOREIGN KEY (IdCinema)
  REFERENCES cinema (IdCinema)
  ON DELETE NO ACTION 
  ON UPDATE NO ACTION;

ALTER TABLE sessao ADD CONSTRAINT fk_sessao_filme FOREIGN KEY (IdFilme)
  REFERENCES filme (IdFilme)
  ON DELETE NO ACTION 
  ON UPDATE NO ACTION;

ALTER TABLE sessao ADD CONSTRAINT fk_sessao_sala FOREIGN KEY (IdSala)
  REFERENCES sala (IdSala)
  ON DELETE NO ACTION 
  ON UPDATE NO ACTION;

