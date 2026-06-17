package org.example.test.entity;


import jakarta.persistence.*;
import lombok.*;

import java.util.List;

@Entity
@Getter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Setter(AccessLevel.NONE)
    private Long id;

    @Setter
    @Column(nullable = false, unique = true)
    private String nickname;

    @Setter
    private int age;

    @Setter
    private String feature;

    @Setter
    @Column(nullable = false)
    private String password;
    @Setter
    private int waveNum;
    @Setter
    private boolean inGame;
    @Setter
    @ElementCollection
    private List<String> equippedPerks;
}

