using System.ComponentModel.DataAnnotations.Schema;

namespace FileRepository.Entities;

public class DataConsistencyCheckEpisode
{
    public int Id { get; set; }
    public int EpisodeId { get; set; }
    public DateTime TimeOfCheck { get; set; }
    public int FilesValidated { get; set; }
    public int FilesDeleted { get; set; }
    [NotMapped] public int TotalFiles => FilesValidated + FilesDeleted;
}