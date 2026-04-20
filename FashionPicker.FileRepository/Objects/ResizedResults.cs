namespace FileRepository.Objects;

public record ResizedResults(
    byte[]? Small,
    byte[]? Medium,
    byte[]? Big,
    byte[]? Original
);