package org.example.test.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.example.test.entity.User;

import java.util.Optional;

public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByNickname(String nickname);
}
