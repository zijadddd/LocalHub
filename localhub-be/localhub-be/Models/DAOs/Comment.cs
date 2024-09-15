﻿namespace localhub_be.Models.DAOs;
public sealed class Comment {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid PostId { get; set; }
    public Post Post { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } 
}
