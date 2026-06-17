package com.example.userapi.dto;

import java.util.List;

public record UserUpdateRequest(
        int waveNum,
        boolean inGame,
        List<String> equippedPerks
) {}