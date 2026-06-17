package com.example.userapi.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import com.example.userapi.entity.User;

import java.util.Optional;

public interface UserRepository extends JpaRepository<User, Long> {
    Optional<User> findByNickname(String nickname);
}
