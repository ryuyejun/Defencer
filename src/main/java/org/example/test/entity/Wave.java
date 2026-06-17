package org.example.test.entity;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

@Entity
@Getter
@Setter
public class Wave {

    @Id
    @GeneratedValue
    private Long id;

    private Long userId;
    private String status;
    private Integer wavenum;
    private LocalDateTime timestamp;

}