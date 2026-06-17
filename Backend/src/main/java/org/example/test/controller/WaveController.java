package org.example.test.controller;

import lombok.RequiredArgsConstructor;
import org.example.test.dto.WaveUpdateRequest;
import org.example.test.service.WaveService;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/wave")
@RequiredArgsConstructor
public class WaveController {

    private final WaveService waveService;

    @PostMapping("/start")
    public ResponseEntity<Void> start() {
        String nickname = SecurityContextHolder.getContext().getAuthentication().getName();
        waveService.save(nickname, "START");
        return ResponseEntity.ok().build();
    }

    @PostMapping("/end")
    public ResponseEntity<Void> end() {
        String nickname = SecurityContextHolder.getContext().getAuthentication().getName();
        waveService.save(nickname, "END");
        return ResponseEntity.ok().build();
    }

    @PostMapping("/update")
    public ResponseEntity<Void> updateWaveNum(@RequestBody WaveUpdateRequest request) {
        String nickname = SecurityContextHolder.getContext().getAuthentication().getName();
        waveService.updateWaveNum(nickname, request.getWavenum());
        return ResponseEntity.ok().build();
    }
}