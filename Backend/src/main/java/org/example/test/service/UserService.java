package org.example.test.service;

import org.example.test.dto.UserCreateRequest;
import org.example.test.dto.UserLoginRequest;
import org.example.test.dto.UserUpdateRequest;
import org.example.test.entity.User;
import org.example.test.exception.DuplicateNicknameException;
import org.example.test.exception.InvalidPasswordException;
import org.example.test.exception.UserNotFoundException;
import org.example.test.repository.UserRepository;
import org.example.test.util.JwtUtil;
import lombok.RequiredArgsConstructor;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class UserService {

    private final UserRepository userRepository;
    private final JwtUtil jwtUtil;
    private final PasswordEncoder passwordEncoder;

    public User register(UserCreateRequest request) {
        if (userRepository.findByNickname(request.nickname()).isPresent()) {
            throw new DuplicateNicknameException("이미 사용 중인 닉네임입니다: " + request.nickname());
        }

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
                .orElseThrow(() -> new UserNotFoundException("유저를 찾을 수 없습니다: " + request.nickname()));

        if (!passwordEncoder.matches(request.password(), user.getPassword())) {
            throw new InvalidPasswordException("비밀번호가 올바르지 않습니다.");
        }

        return jwtUtil.generateToken(user.getNickname());
    }

    public User createUser(UserCreateRequest request) {
        if (userRepository.findByNickname(request.nickname()).isPresent()) {
            throw new DuplicateNicknameException("이미 사용 중인 닉네임입니다: " + request.nickname());
        }

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
                .orElseThrow(() -> new UserNotFoundException("유저를 찾을 수 없습니다. id: " + id));
    }

    public User updateUser(Long id, UserUpdateRequest request) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new UserNotFoundException("유저를 찾을 수 없습니다. id: " + id));

        user.setWaveNum(request.waveNum());
        user.setInGame(request.inGame());
        user.setEquippedPerks(request.equippedPerks());

        return userRepository.save(user);
    }

    public void deleteUser(Long id) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new UserNotFoundException("유저를 찾을 수 없습니다. id: " + id));
        userRepository.delete(user);
    }
}