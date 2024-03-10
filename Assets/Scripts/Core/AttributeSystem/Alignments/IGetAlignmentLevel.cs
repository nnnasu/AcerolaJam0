
using Core.AttributeSystem.Alignments;

public interface IGetAlignmentLevel {
    public (AlignmentDefinition, int) GetAlignmentLevel(int level);
}