package org.example.test.repository;

import org.example.test.entity.Wave;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface WaveRepository extends JpaRepository<Wave, Long> {
    Optional<Wave> findTopByUserIdOrderByTimestampDesc(Long userId);
}