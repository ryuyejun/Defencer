package com.example.userapi.service;

import com.example.userapi.dto.UserCreateRequest; /*완료*/
import com.example.userapi.dto.UserLoginRequest;
import com.example.userapi.dto.UserUpdateRequest;
import com.example.userapi.repository.UserRepository;/*완료*/
import com.example.userapi.util.JwtUtil;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor; /*완료*/
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service; /*완료*/
import com.example.userapi.entity.User;
import java.util.List;
@Service
@RequiredArgsConstructor
public class UserService {

    private final UserRepository userRepository;
    private final JwtUtil jwtUtil;
    private final PasswordEncoder passwordEncoder;

    public User register(UserCreateRequest request) {
        User user = User.builder()
                .nickname(request.nickname())
                .password(passwordEncoder.encode(request.password()))

                .waveNum(1)
                .inGame(false)
                .equippedPerks(new java.util.ArrayList<>())

                .build();

        return userRepository.save(user);
    }

    public String login(UserLoginRequest request) {
        User user = userRepository.findByNickname(request.nickname())
                .orElseThrow(() -> new RuntimeException("유저를 찾을 수 없습니다."));
        if (!passwordEncoder.matches(request.password(), user.getPassword())) {
            throw new RuntimeException("비밀번호가 올바르지 않습니다.");
        }

        return jwtUtil.generateToken(user.getNickname());
    }
    public User createUser(UserCreateRequest request) {
        User user = User.builder()
                .nickname(request.nickname())
                .build();
        return userRepository.save(user);
    }
    public List<User> getAllUsers() {
        return userRepository.findAll();
    }

    public User getUserById(Long id) {
        return userRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("유저를 찾을 수 없습니다. id:" + id));
    }

    public User updateUser(Long id, UserUpdateRequest request) {

        User user = userRepository.findById(id)
                .orElseThrow(() ->
                        new RuntimeException("유저를 찾을 수 없습니다. id: " + id));

        user.setWaveNum(request.waveNum());
        user.setInGame(request.inGame());
        user.setEquippedPerks(request.equippedPerks());

        return userRepository.save(user);
    }

    public void deleteUser(Long id) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("유저를 찾을 수 없습니다. id: " + id));
        userRepository.delete(user);
    }
}
