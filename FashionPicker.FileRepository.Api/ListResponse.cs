namespace FileRepository.Api;

public record ListResponse<TObject>(
    IEnumerable<TObject> Data
);