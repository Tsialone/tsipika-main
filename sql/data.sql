drop table joueur CASCADE;
drop table scoreJoueur CASCADE;
drop table manche  CASCADE;  



CREATE TABLE joueur(
    idJoueur   serial PRIMARY KEY, 
    nom VARCHAR(255) NOT NULL
);
CREATE TABLE manche (
    idManche serial PRIMARY KEY ,
    dateManche TIMESTAMP 
);

CREATE TABLE  scoreJoueur(
    score INT NOT NULL,
    idJoueur INT NOT NULL,
    dateScore TIMESTAMP NOT NULL,
    FOREIGN KEY (idJoueur) REFERENCES joueur(idJoueur)
);
    INSERT INTO joueur (nom) VALUES ('Marc');
    INSERT INTO joueur (nom) VALUES ('Luc');

INSERT INTO scoreJoueur (idJoueur, score, dateScore) VALUES (1, 0, CURRENT_TIMESTAMP);
INSERT INTO scoreJoueur (idJoueur, score, dateScore) VALUES (2, 0, CURRENT_TIMESTAMP);





