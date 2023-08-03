CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8mb4 COMMENT '';

-- SECTION ALBUMS

-- NOTE creatorId(255) below needs to match exactly the id from accounts above or else we won't be able to compare/connect the two.

-- NOTE foreign key/virtual is the name of the property in the current table (creatorId), references where ever we get that creatorId from (accounts(id))

CREATE TABLE
    albums(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        title VARCHAR(50) NOT NULL,
        category VARCHAR(50) NOT NULL,
        coverImg VARCHAR(255) NOT NULL,
        archived BOOLEAN NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

ALTER TABLE
    albums MODIFY COLUMN createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'this is when this was created';

-- ADD

--     COLUMN updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

ALTER TABLE pictures
ADD
    COLUMN createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
ADD
    COLUMN updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;

DELETE FROM accounts WHERE id = '634844a08c9d1ba02348913d';

INSERT INTO
    albums (
        title,
        category,
        coverImg,
        archived,
        creatorId
    )
VALUES (
        'Just Muscles',
        'mollusks',
        'https://plus.unsplash.com/premium_photo-1668995975042-6344a48aef90?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8c3F1aWR8ZW58MHx8MHx8&auto=format&fit=crop&w=500&q=60',
        false,
        '631b5b5fa7f0b66bb817725a'
    );

SELECT
    alb.id,
    alb.title,
    creator.name
FROM albums alb
    JOIN accounts creator ON creator.id = alb.creatorId
WHERE alb.category = 'mollusks';

SELECT alb.*, creator.*
FROM albums alb
    JOIN accounts creator ON creator.id = alb.creatorId;

--  SECTION PICTURES

CREATE TABLE
    pictures(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        imgUrl VARCHAR(255) NOT NULL,
        albumId INT NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (albumId) REFERENCES albums(id) ON DELETE CASCADE,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8mb4 COMMENT '';

INSERT INTO
    pictures (imgUrl, albumId, creatorId)
VALUES
    -- ('https://images.unsplash.com/photo-1599400263719-81fd498622c4?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80',4,'631b5b5fa7f0b66bb817725a');
    -- ('https://images.unsplash.com/photo-1615057956902-e181da52650b?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1503&q=80',4,'634844a08c9d1ba02348913d');
    -- ('https://images.unsplash.com/photo-1608375926864-1db4626a9f5f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80',4,'631b5b5fa7f0b66bb817725a'); (
    'https://images.unsplash.com/photo-1625556580790-7ce9101965b1?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1074&q=80',
    3,
    '631b5b5fa7f0b66bb817725a'
);

SELECT
    pic.*,
    alb.*,
    creator.*,
    aCreator.*
FROM pictures pic
    JOIN albums alb ON alb.id = pic.albumId
    JOIN accounts creator ON pic.creatorId = creator.id
    JOIN accounts aCreator ON alb.creatorId = aCreator.id;

-- SECTION COLLABORATORS

CREATE TABLE
    collaborators(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'this is the primary key for this table',
        albumId INT NOT NULL,
        accountId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        FOREIGN KEY(albumId) REFERENCES albums(id) ON DELETE CASCADE,
        FOREIGN KEY(accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        UNIQUE(albumId, accountId)
    ) default charset utf8mb4 COMMENT '';

INSERT INTO
    collaborators(albumId, accountId)
VALUES (
        '4',
        '644a95d5acec568e63f2e2f1'
    );

DROP TABLE collaborators;

SELECT clab.*, acct.*
FROM collaborators clab
    JOIN accounts acct ON acct.id = clab.accountId
WHERE clab.albumId = 3;

SELECT alb.*, acct.*, clab.*
FROM collaborators clab
    JOIN albums alb ON alb.id = clab.albumId
    JOIN accounts acct ON acct.id = alb.creatorId
WHERE
    clab.accountId = '634844a08c9d1ba02348913d';