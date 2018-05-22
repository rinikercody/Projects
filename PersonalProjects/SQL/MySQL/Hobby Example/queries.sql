#Get oldest user
SELECT MAX(age) FROM users;

#Orders users by decreasing age
SELECT username,age FROM users ORDER BY age desc; 

#Simple Join example
SELECT u.username, h.hobby_name FROM users u
JOIN users_hobbies uh ON uh.user_id = u.user_id
JOIN hobbies h ON h.hobby_id = uh.hobby_id;

#Order hobbies by how many users have them
SELECT h.hobby_name, COUNT(*) count 
FROM users_hobbies uh
JOIN HOBBIES h ON h.hobby_id = uh.hobby_id 
GROUP BY uh.hobby_id
ORDER BY count desc; 

#Gets the most popular hobby
SELECT x.hobby_name, MAX(counts) most_common_hobby FROM
(
	SELECT h.hobby_name, COUNT(*) counts 
	FROM users_hobbies uh
	JOIN hobbies h ON h.hobby_id = uh.hobby_id 
	GROUP BY uh.hobby_id 
) AS x;

#Get users with multiple hobbies
SELECT x.username, counts FROM
(
	SELECT u.username, COUNT(*) counts FROM users_hobbies uh
	JOIN users u ON u.user_id = uh.user_id
	GROUP BY uh.user_id
) AS x
WHERE counts > 1;