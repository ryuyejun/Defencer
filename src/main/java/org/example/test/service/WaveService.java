package org.example.test.service;

import lombok.RequiredArgsConstructor;
import org.example.test.entity.User;
import org.example.test.entity.Wave;
import org.example.test.repository.UserRepository;
import org.example.test.repository.WaveRepository;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;

@Service
@RequiredArgsConstructor
public class WaveService {

    private final WaveRepository waveRepository;
    private final UserRepository userRepository;

    public void save(String nickname, String status) {
        User user = userRepository.findByNickname(nickname)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + nickname));

        Wave wave = new Wave();
        wave.setUserId(user.getId());
        wave.setStatus(status);
        wave.setTimestamp(LocalDateTime.now());
        waveRepository.save(wave);

        if ("START".equals(status)) {
            user.setInGame(true);
        } else if ("END".equals(status)) {
            user.setInGame(false);
        }
        userRepository.save(user);
    }

    public void updateWaveNum(String nickname, Integer wavenum) {
        User user = userRepository.findByNickname(nickname)
                .orElseThrow(() -> new IllegalArgumentException("사용자를 찾을 수 없습니다: " + nickname));

        user.setWaveNum(wavenum);
        userRepository.save(user);
    }
}