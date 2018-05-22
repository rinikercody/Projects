DROP TABLE IF EXISTS USERS_HOBBIES;
DROP TABLE IF EXISTS HOBBIES;
DROP TABLE IF EXISTS USERS;

CREATE TABLE users(
	user_id INT PRIMARY KEY,
	username VARCHAR(50) NOT NULL,
	age INT,
	phone_number VARCHAR(50),
	email VARCHAR(50)
);

INSERT INTO users VALUES(1,'Cody',22,'785-844-0207','rinikercody@gmail.com');
INSERT INTO users VALUES(2,'John',34,'785-555-1234','JohnJ@yahoo.com');
INSERT INTO users VALUES(3,'Ann',56,'785-555-7020','AnnA@gmail.com');
INSERT INTO users VALUES(4,'Eric',28,'785-555-6753','EricE@hotmail.com');
INSERT INTO users VALUES(5,'Tim',42,'785-555-9876','TimT@gmail.com');
INSERT INTO users VALUES(6,'Jake',18,'785-555-1111','JakeJ@yahoo.com');

CREATE TABLE hobbies(
	hobby_id INT PRIMARY KEY,
	hobby_name VARCHAR(50) NOT NULL
);

INSERT INTO hobbies VALUES(1,"Biking");
INSERT INTO hobbies VALUES(2,"Running");
INSERT INTO hobbies VALUES(3,"Games");

CREATE TABLE users_hobbies(
	user_id INT,
	FOREIGN KEY(user_id) REFERENCES USERS(user_id),
	hobby_id INT,
	FOREIGN KEY(hobby_id) REFERENCES HOBBIES(hobby_id)
);

INSERT INTO users_hobbies VALUES(1,3);
INSERT INTO users_hobbies VALUES(1,1);

INSERT INTO users_hobbies VALUES(2,2);
INSERT INTO users_hobbies VALUES(2,1);

INSERT INTO users_hobbies VALUES(3,2);
INSERT INTO users_hobbies VALUES(4,1);
INSERT INTO users_hobbies VALUES(5,1);
INSERT INTO USERS_HOBBIES VALUES(6,1);
